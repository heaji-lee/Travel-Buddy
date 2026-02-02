import { Component, inject, OnInit, signal } from '@angular/core';
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

import { TripsService } from '../../services/trips.service';
import { Trip } from '../../models/trips.models';

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
    ],
    templateUrl: './trips-control.component.html',
    styleUrl: './trips-control.component.css',
})
export class TripsControlComponent implements OnInit {
    private readonly route = inject(ActivatedRoute);
    private readonly fb = inject(FormBuilder);
    private readonly tripsService = inject(TripsService);
    private readonly router = inject(Router);

    savedTripValue = signal<Trip | null>(null);
    formIsAltered = signal(false);

    form: FormGroup;
    isExistingTrip = false;
    title = '';
    subTitle = '';
    submitLabel = '';

    // gets the id from url /trips/:id
    id = this.route.snapshot.paramMap.get('id')?.toString() || 'new';

    constructor() {
        this.form = this.fb.group({
            name: ['', Validators.required],
            city: ['', Validators.required],
            country: ['', Validators.required],
            startAt: ['', Validators.required],
            endAt: ['', Validators.required],
        });
    }

    ngOnInit() {
        if (this.id && this.id !== 'new') {
            this.tripsService.getTripById(this.id).subscribe({
                next: (trip: Trip) => {
                    this.savedTripValue.set(trip);
                    this.isExistingTrip = true;
                    this.form.patchValue({
                        ...trip,
                        startAt: trip.startAt ? new Date(trip.startAt) : null,
                        endAt: trip.endAt ? new Date(trip.endAt) : null,
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
        const submitAction = this.isExistingTrip
            ? this.tripsService.updateTrip(this.id, { ...this.form.value, id: this.id })
            : this.tripsService.createTrip(this.form.value);

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
            next: (trip: Trip) => {
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
