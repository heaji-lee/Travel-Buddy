import {
    Component,
    Input,
    Output,
    EventEmitter,
    inject,
    SimpleChanges,
    signal,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule, FormArray } from '@angular/forms';

import { DrawerModule } from 'primeng/drawer';
import { ButtonModule } from 'primeng/button';
import { AccordionModule } from 'primeng/accordion';
import { DatePickerModule } from 'primeng/datepicker';
import { MultiSelectModule } from 'primeng/multiselect';
import { TableModule } from 'primeng/table';
import { InputTextModule } from 'primeng/inputtext';
import { TextareaModule } from 'primeng/textarea';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { MeterGroupModule } from 'primeng/metergroup';
import { InputNumberModule } from 'primeng/inputnumber';
import { FloatLabelModule } from 'primeng/floatlabel';

import { Trip } from '../../modules/trips/models/trips.models';
import { TRIP_ITINERARIES } from '../../shared/constants';
import { TripsService } from '../../modules/trips/services/trips.service';
import { Destination } from '../../modules/trips/models/destinations.models';

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
        ReactiveFormsModule,
        TableModule,
        InputTextModule,
        TextareaModule,
        AutoCompleteModule,
        MeterGroupModule,
        InputNumberModule,
        FloatLabelModule
    ],
    templateUrl: './trip-drawer.component.html',
    styleUrl: './trip-drawer.component.css',
})
export class TripDrawerComponent {
    private readonly fb = inject(FormBuilder);
    private readonly tripsServices = inject(TripsService);

    title = '';
    subTitle = '';
    submitLabel = '';
    destinations = signal<Destination[]>([]);

    @Input() visible = false;
    @Input() selectedTrip!: Trip | null;
    @Input() companions: any[] = [];
    @Input() interests: any[] = [];
    @Input() travelStyles: any[] = [];

    @Output() visibleChange = new EventEmitter<boolean>();
    @Output() submit = new EventEmitter<any>();

    startAt = signal<Date | null>(null);
    endAt = signal<Date | null>(null);

    form!: FormGroup;

    ngOnChanges(changes: SimpleChanges) {
        if (changes['selectedTrip']) {
            const trip = changes['selectedTrip'].currentValue as Trip | null;
            this.buildForm(trip);
        }
    }

    private buildForm(trip: Trip | null) {
        this.startAt.set(trip?.startAt ? new Date(trip.startAt) : null);
        this.endAt.set(trip?.endAt ? new Date(trip.endAt) : null);
        this.form = this.fb.group({
            name: [trip?.name || '', Validators.required],
            destination: [
                trip
                    ? {
                          city: trip.city,
                          country: trip.country,
                          display: `${trip.city}, ${trip.country}`,
                      }
                    : null,
                Validators.required,
            ],
            startAt: [this.startAt(), Validators.required],
            endAt: [this.endAt(), Validators.required],
            companions: [trip?.companions?.map((c) => c.id) || []],
            travelStyles: [trip?.travelStyles?.map((t) => t.id) || []],
            interests: [trip?.interests?.map((i) => i.id) || []],
            tripItineraries: this.fb.array(
                trip?.tripItineraries?.map((day) =>
                    this.fb.group({
                        dayNumber: [day.dayNumber, Validators.required],
                        notes: [day.notes || ''],
                    }),
                ) || [],
            ),
            totalBudget: [trip?.totalBudget || '', Validators.required]
        });

        this.form.get('startAt')?.valueChanges.subscribe((date) => {
            this.startAt.set(date);
            this.updateItineraries();
        });

        this.form.get('endAt')?.valueChanges.subscribe((date) => {
            this.endAt.set(date);
            this.updateItineraries();
        });

        if (!trip?.tripItineraries?.length) {
            this.updateItineraries();
        }

        this.setInitialValues(!!trip?.id);
    }

    private updateItineraries() {
        const start = this.startAt();
        const end = this.endAt();

        if (!start || !end) {
            this.form.setControl(TRIP_ITINERARIES, this.fb.array([]));
            return;
        }

        const startDate = new Date(start);
        const endDate = new Date(end);

        if (endDate < startDate) {
            this.form.setControl(TRIP_ITINERARIES, this.fb.array([]));
            return;
        }

        const days =
            Math.ceil((endDate.getTime() - startDate.getTime()) / (1000 * 60 * 60 * 24)) + 1;
        const existingNotes = (this.form.get(TRIP_ITINERARIES) as FormArray)?.value || [];

        const tripItinerariesArray = this.fb.array(
            Array.from({ length: days }, (_, i) =>
                this.fb.group({
                    dayNumber: [i + 1, Validators.required],
                    notes: [existingNotes[i]?.notes || ''],
                }),
            ),
        );

        this.form.setControl(TRIP_ITINERARIES, tripItinerariesArray);
    }

    get tripItinerariesArray(): FormArray {
        return this.form.get(TRIP_ITINERARIES) as FormArray;
    }

    private setInitialValues(isExistingTrip: boolean) {
        this.title = isExistingTrip ? 'Edit Trip' : 'New Trip';
        this.subTitle = isExistingTrip
            ? 'Modify the details of the trip.'
            : 'Enter the details of the new trip.';
        this.submitLabel = isExistingTrip ? 'Update Trip' : 'Create Trip';
    }

    getDateForDay(dayNumber: number | null): Date | null {
        if (!dayNumber || !this.form.get('startAt')?.value) return null;
        const startDate = new Date(this.form.get('startAt')!.value);
        startDate.setDate(startDate.getDate() + dayNumber - 1);
        return startDate;
    }

    searchDestinations(event: any) {
        const query = event.query?.trim();
        if (query.length < 2) {
            this.destinations.set([]);
            return;
        }
        this.tripsServices.getDestinations(query).subscribe((results) => {
            const destinations = results.map((d) => ({
                ...d,
                display: `${d.city}, ${d.country}`,
            }));
            this.destinations.set(destinations);
        });
    }

    onSubmit() {
        if (this.form.invalid) return;

        const formValue = this.form.value;
        const destination: Destination = formValue.destination;

        const payload = {
            ...formValue,
            city: destination.city,
            country: destination.country,
        };

        this.submit.emit(payload);
    }

    close() {
        this.visibleChange.emit(false);
    }
}
