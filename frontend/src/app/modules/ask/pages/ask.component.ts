import { CommonModule } from '@angular/common';
import { ApplicationRef, ChangeDetectorRef, Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { AutoCompleteModule, AutoCompleteCompleteEvent } from 'primeng/autocomplete';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { TextareaModule } from 'primeng/textarea';
import { TripsService } from '../../trips/services/trips.service';
import { AskService } from '../services/ask.services';

@Component({
    selector: 'app-ask',
    standalone: true,
    imports: [
        CommonModule,
        ReactiveFormsModule,
        AutoCompleteModule,
        ButtonModule,
        InputTextModule,
        TextareaModule,
    ],
    templateUrl: './ask.component.html',
    styleUrl: './ask.component.css',
})
export class AskComponent {
    private readonly fb = inject(FormBuilder);
    private readonly tripsService = inject(TripsService);
    private readonly askService = inject(AskService);
    private readonly cdr = inject(ChangeDetectorRef);
    private readonly appRef = inject(ApplicationRef);

    tripForm = this.fb.nonNullable.group({
        city: ['', [Validators.required, Validators.minLength(2)]],
        days: [3, [Validators.required, Validators.min(1), Validators.max(14)]],
        preferences: [''],
    });

    itinerary: string[] = [];
    isGenerating = false;
    generated = false;
    submittedCity = '';
    submittedDays = 0;
    citySuggestions: string[] = [];

    searchCities(event: AutoCompleteCompleteEvent) {
        const query = event.query?.trim() ?? '';

        if (query.length < 2) {
            this.citySuggestions = [];
            return;
        }

        this.tripsService.getDestinations(query).subscribe((results) => {
            this.citySuggestions = results.map(
                (destination) => `${destination.city}, ${destination.country}`,
            );
        });
    }

    onCitySelect(selectedValue: string) {
        const cityName = selectedValue?.split(',')[0]?.trim();
        if (cityName) {
            this.tripForm.patchValue({ city: cityName });
        }
    }

    onGenerate() {
        if (this.tripForm.invalid) {
            this.tripForm.markAllAsTouched();
            return;
        }

        this.isGenerating = true;
        this.generated = false;
        this.itinerary = [];

        const { city, days, preferences } = this.tripForm.getRawValue();
        this.submittedCity = city;
        this.submittedDays = days;

        this.askService.generateItinerary({ city, days, preferences }).subscribe({
            next: (response) => {
                this.itinerary = response.itinerary;
                this.isGenerating = false;
                this.generated = true;
                this.cdr.detectChanges();
                this.appRef.tick();
            },
            error: () => {
                this.isGenerating = false;
                this.cdr.detectChanges();
                this.appRef.tick();
            },
        });
    }

    resetForm() {
        this.tripForm.reset({ city: '', days: 3, preferences: '' });
        this.itinerary = [];
        this.generated = false;
        this.isGenerating = false;
    }
}
