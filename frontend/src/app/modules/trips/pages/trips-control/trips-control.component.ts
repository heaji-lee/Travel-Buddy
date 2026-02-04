import { Component, computed, inject, OnInit, signal } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs/internal/Observable';

import { InputTextModule } from 'primeng/inputtext';
import { AccordionModule } from 'primeng/accordion';
import { ButtonModule } from 'primeng/button';
import { DatePickerModule } from 'primeng/datepicker';
import { SelectModule } from 'primeng/select';
import { ChipModule } from 'primeng/chip';
import { MultiSelectModule } from 'primeng/multiselect';

import { TripsService } from '../../services/trips.service';
import { Trip } from '../../models/trips.models';
import { CompanionsService } from '../../../manage/services/companions.services';
import { InterestsService } from '../../../manage/services/interests.services';
import { TravelStylesService } from '../../../manage/services/travelStyles.services';
import { Companion, Interest, TravelStyle } from '../../../manage/models/manage.models';

@Component({
    selector: 'app-trips-control',
    imports: [
        InputTextModule,
        AccordionModule,
        ButtonModule,
        DatePickerModule,
        SelectModule,
        ReactiveFormsModule,
        CommonModule,
        ChipModule,
        MultiSelectModule,
    ],
    templateUrl: './trips-control.component.html',
    styleUrl: './trips-control.component.css',
})
export class TripsControlComponent implements OnInit {
    private readonly route = inject(ActivatedRoute);
    private readonly fb = inject(FormBuilder);
    private readonly tripsService = inject(TripsService);
    private readonly router = inject(Router);
    private readonly companionsService = inject(CompanionsService);
    private readonly interestsService = inject(InterestsService);
    private readonly travelStylesService = inject(TravelStylesService);

    savedTripValue = signal<Trip | null>(null);
    formIsAltered = signal(false);

    form: FormGroup;
    isExistingTrip = false;
    title = '';
    subTitle = '';
    submitLabel = '';

    pageSize = 5;

    companionsPage = signal(1);
    companionsSkip = computed(() => (this.companionsPage() - 1) * this.pageSize);
    interestsPage = signal(1);
    interestsSkip = computed(() => (this.interestsPage() - 1) * this.pageSize);
    travelStylesPage = signal(1);
    travelStylesSkip = computed(() => (this.travelStylesPage() - 1) * this.pageSize);

    // gets the id from url /trips/:id
    id = this.route.snapshot.paramMap.get('id')?.toString() || 'new';

    constructor() {
        this.form = this.fb.group({
            name: ['', Validators.required],
            city: ['', Validators.required],
            country: ['', Validators.required],
            startAt: ['', Validators.required],
            endAt: ['', Validators.required],
            companions: [[]],
            interests: [[]],
            travelStyles: [[]]
        });
    }

    companions: Companion[] = [];
    travelStyles: TravelStyle[] = [];
    interests: Interest[] = [];

    ngOnInit() {
        this.companionsService
            .getPaginatedCompanions(this.companionsSkip(), this.pageSize)
            .subscribe({
                next: (response) => {
                    this.companions = response.items;
                },
                error: (error) => {
                    console.error('Error fetching companions:', error);
                },
            });
        this.travelStylesService
            .getPaginatedTravelStyles(this.travelStylesSkip(), this.pageSize)
            .subscribe({
                next: (response) => {
                    this.travelStyles = response.items;
                },
                error: (error) => {
                    console.error('Error fetching travelStyles:', error);
                },
            });
        this.interestsService.getPaginatedInterests(this.interestsSkip(), this.pageSize).subscribe({
            next: (response) => {
                this.interests = response.items;
            },
            error: (error) => {
                console.error('Error fetching interests:', error);
            },
        });

        if (this.id && this.id !== 'new') {
            this.tripsService.getTripById(this.id).subscribe({
                next: (trip: Trip) => {
                    this.savedTripValue.set(trip);
                    this.isExistingTrip = true;
                    this.form.patchValue({
                        ...trip,
                        startAt: trip.startAt ? new Date(trip.startAt) : null,
                        endAt: trip.endAt ? new Date(trip.endAt) : null,
                        companions: trip.companions?.map(c => c.id) || [],
                        interests: trip.interests?.map(i => i.id) || [],
                        travelStyles: trip.travelStyles?.map(t => t.id) || []
                    });
                    this.setInitialValues();
                },
                error: (error: any) => {
                    console.error('Error fetching trip:', error);
                },
            });
        } else {
            this.setInitialValues();
        }

        this.form.valueChanges.subscribe(() => {
            this.formIsAltered.set(this.form.dirty);
        });
    }

    onSubmit() {
        const payload = {
            ...this.form.value,
            companionIds: this.form.value.companions.map((c: Companion) => c.id),
            interestIds: this.form.value.interests.map((c: Interest) => c.id),
            travelStyleIds: this.form.value.travelStyles.map((c: TravelStyle) => c.id),
        };

        const submitAction = this.isExistingTrip
            ? this.tripsService.updateTrip(this.id, { ...payload, id: this.id })
            : this.tripsService.createTrip(payload);

        this.handleSubmitAction(submitAction);
    }

    private setInitialValues() {
        this.title = this.isExistingTrip ? 'Edit Trip' : 'New Trip';
        this.subTitle = this.isExistingTrip
            ? 'Modify the details of the trip.'
            : 'Enter the details of the new trip.';
        this.submitLabel = this.isExistingTrip ? 'Update Trip' : 'Create Trip';
    }

    private handleSubmitAction(submitAction: Observable<Trip>) {
        submitAction.subscribe({
            next: () => {
                this.navigateToTripsList();
            },
            error: (error: any) => {
                console.error('Error submitting trip form:', error);
            },
        });
    }

    navigateToTripsList() {
        this.router.navigate(['/trips']);
    }
}
