import { Component, Input, Output, EventEmitter, inject, SimpleChanges } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';

import { DrawerModule } from 'primeng/drawer';
import { ButtonModule } from 'primeng/button';
import { AccordionModule } from 'primeng/accordion';
import { DatePickerModule } from 'primeng/datepicker';
import { MultiSelectModule } from 'primeng/multiselect';

import { Trip } from '../../modules/trips/models/trips.models';

@Component({
    selector: 'app-trip-drawer',
    standalone: true,
    imports: [
        CommonModule,
        DrawerModule,
        ButtonModule,
        AccordionModule,
        DatePickerModule,
        MultiSelectModule,
        ReactiveFormsModule
    ],
    templateUrl: './trip-drawer.component.html',
    styleUrl: './trip-drawer.component.css',
})
export class TripDrawerComponent {
    private readonly fb = inject(FormBuilder);

    title = '';
    subTitle = '';
    submitLabel = '';

    @Input() visible = false;
    @Input() selectedTrip!: Trip | null;
    @Input() companions: any[] = [];
    @Input() interests: any[] = [];
    @Input() travelStyles: any[] = [];

    @Output() visibleChange = new EventEmitter<boolean>();
    @Output() submit = new EventEmitter<any>();

    form!: FormGroup;

    ngOnChanges(changes: SimpleChanges) {
        if (changes['selectedTrip']) {
            const trip = changes['selectedTrip'].currentValue as Trip | null;
            
            if (trip?.id) {
                this.form = this.fb.group({
                    name: [trip.name, Validators.required],
                    city: [trip.city, Validators.required],
                    startAt: [new Date(trip.startAt), Validators.required],
                    endAt: [new Date(trip.endAt), Validators.required],
                    companions: [(trip.companions ?? []).map(c => c.id)],
                    travelStyles: [(trip.travelStyles ?? []).map(c => c.id)],
                    interests: [(trip.interests ?? []).map(c => c.id)],
                });
                this.setInitialValues(true);
            } else {
                this.form = this.fb.group({
                    name: ['', Validators.required],
                    city: ['', Validators.required],
                    startAt: ['', Validators.required],
                    endAt: ['', Validators.required],
                    companions: [[]],
                    travelStyles: [[]],
                    interests: [[]],
                });
                this.setInitialValues(false);
            }
        }
    }

    private setInitialValues(isExistingTrip: boolean) {
        this.title = isExistingTrip ? 'Edit Trip' : 'New Trip';
        this.subTitle = isExistingTrip
            ? 'Modify the details of the trip.'
            : 'Enter the details of the new trip.';
        this.submitLabel = isExistingTrip ? 'Update Trip' : 'Create Trip';
    }

    onSubmit() {
        if (this.form.valid) {
            this.submit.emit(this.form.value);
        }
    }

    close() {
        this.visibleChange.emit(false);
    }
}
