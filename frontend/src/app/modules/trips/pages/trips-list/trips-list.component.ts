import { Component, computed, inject, resource, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { CommonModule } from '@angular/common';

import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { PaginatorModule } from 'primeng/paginator';
import { DialogModule } from 'primeng/dialog';
import { DrawerModule } from 'primeng/drawer';
import { MessageService } from 'primeng/api';

import { TripsService } from '../../services/trips.service';
import { CompanionsService } from '../../../manage/services/companions.services';
import { InterestsService } from '../../../manage/services/interests.services';
import { TravelStylesService } from '../../../manage/services/travelStyles.services';
import { PAGE_SIZE } from '../../../../shared/constants';
import { TripsApiResponse, Trip } from '../../models/trips.models';
import { TripDrawerComponent } from '../../../../components/trip-drawer/trip-drawer.component';

@Component({
    selector: 'app-trips-list',
    imports: [
        TableModule,
        CommonModule,
        ButtonModule,
        PaginatorModule,
        DialogModule,
        DrawerModule,
        TripDrawerComponent,
    ],
    templateUrl: './trips-list.component.html',
    styleUrl: './trips-list.component.css',
})
export class TripsListComponent {
    private readonly tripsService = inject(TripsService);
    private readonly companionsService = inject(CompanionsService);
    private readonly interestsService = inject(InterestsService);
    private readonly travelStylesService = inject(TravelStylesService);
    private readonly messageService = inject(MessageService);

    page = signal(1);
    pageSize = PAGE_SIZE;
    skip = computed(() => (this.page() - 1) * PAGE_SIZE);
    tripId: string = '';
    isDeleteDialogVisible = false;
    tripToDeleteId = '';
    isDrawOpen = false;
    selectedTrip = signal<Trip | null>(null);

    companions = signal<any[]>([]);
    interests = signal<any[]>([]);
    travelStyles = signal<any[]>([]);

    trips = resource({
        loader: () => firstValueFrom(this.tripsService.getPaginatedTrips(this.skip(), PAGE_SIZE)),
    });

    constructor() {
        this.companionsService.getPaginatedCompanions(0, 100).subscribe((response) => {
            this.companions.set(response.items);
        });
        this.interestsService.getPaginatedInterests(0, 100).subscribe((response) => {
            this.interests.set(response.items);
        });
        this.travelStylesService.getPaginatedTravelStyles(0, 100).subscribe((response) => {
            this.travelStyles.set(response.items);
        });
    }

    totalRecords = computed(() => {
        const value = this.trips.value();
        return (value as any as TripsApiResponse)?.total || 0;
    });

    onPageChange(event: any) {
        this.page.set(event.page + 1);
        this.trips.reload();
    }

    deleteTrip(tripId: string) {
        if (!this.tripToDeleteId) return;
        this.tripsService.deleteTrip(tripId).subscribe(() => {
            this.trips.reload();
            this.isDeleteDialogVisible = false;
        });
    }

    showDeleteDialog(tripId: string) {
        this.tripToDeleteId = tripId;
        this.isDeleteDialogVisible = true;
    }

    openDraw(trip?: Trip) {
        if (trip?.id) {
            this.tripsService.getTripById(trip.id.toString()).subscribe((fullTrip) => {
                this.selectedTrip.set(fullTrip);
                this.isDrawOpen = true;
            });
        } else {
            this.selectedTrip.set(null);
            this.isDrawOpen = true;
        }
    }

    closeDraw() {
        this.isDrawOpen = false;
        this.selectedTrip.set(null);
    }

    onTripSubmit(formValue: any) {
        const payload = {
            name: formValue.name,
            city: formValue.city,
            startAt: formValue.startAt,
            endAt: formValue.endAt,
            companionIds: formValue.companions,
            travelStyleIds: formValue.travelStyles,
            interestIds: formValue.interests,
            tripItineraries: formValue.tripItineraries.map((itinerary: any) => ({
                dayNumber: itinerary.dayNumber,
                notes: itinerary.notes,
            })),
        };
        const editingTrip = this.selectedTrip();
        const isUpdate = !!editingTrip;

        if (editingTrip) {
            this.tripsService.updateTrip(editingTrip!.id!, payload).subscribe({
                next: () => {
                    this.showToastSuccess(isUpdate);
                    this.closeDraw();
                    this.trips.reload();
                },
                error: () => {
                    this.showToastError();
                },
            });
        } else {
            this.tripsService.createTrip(payload).subscribe({
                next: () => {
                    this.showToastSuccess(isUpdate);
                    this.closeDraw();
                    this.trips.reload();
                },
                error: () => {
                    this.showToastError();
                },
            });
        }
    }

    showToastSuccess(isUpdate: boolean) {
        this.messageService.add({
            key: 'globalToast',
            severity: 'success',
            summary: isUpdate ? 'Trip updated' : 'Trip created',
            detail: isUpdate ? 'Your changes have been saved' : 'Your trip is ready to go',
            life: 3000,
        });
    }

    showToastError() {
        this.messageService.add({
            key: 'globalToast',
            severity: 'error',
            summary: 'Something went wrong',
            detail: 'Please try again later',
            life: 3000,
        });
    }
}
