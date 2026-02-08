import { Component, Input, Output, EventEmitter, inject, SimpleChanges } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';

import { DrawerModule } from 'primeng/drawer';
import { ButtonModule } from 'primeng/button';
import { AccordionModule } from 'primeng/accordion';
import { DatePickerModule } from 'primeng/datepicker';
import { MultiSelectModule } from 'primeng/multiselect';

import { Trip } from '../../modules/trips/models/trips.models';
import { TripFormComponent } from '../trip-form/trip-form.component';

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
        TripFormComponent,
    ],
    templateUrl: './trip-drawer.component.html',
    styleUrl: './trip-drawer.component.css',
})
export class TripDrawerComponent {
    private readonly fb = inject(FormBuilder);
    @Input() visible = false;
    @Input() selectedTrip!: Trip | null;
    @Input() companions: any[] = [];
    @Input() interests: any[] = [];
    @Input() travelStyles: any[] = [];
    @Output() visibleChange = new EventEmitter<boolean>();

    form!: FormGroup;

    ngOnChanges(changes: SimpleChanges) {
        if (changes['selectedTrip']?.currentValue) {
            const trip = changes['selectedTrip'].currentValue as Trip;

            this.form = this.fb.group({
                name: [trip.name, Validators.required],
                city: [trip.city, Validators.required],
                startAt: [trip.startAt, Validators.required],
                endAt: [trip.endAt, Validators.required],
                companions: [trip.companions ?? []],
                travelStyles: [trip.travelStyles ?? []],
                interests: [trip.interests ?? []],
            });
        }
    }

    save() {
        console.log(this.form.value);
        this.close();
    }

    close() {
        this.visibleChange.emit(false);
    }
}
