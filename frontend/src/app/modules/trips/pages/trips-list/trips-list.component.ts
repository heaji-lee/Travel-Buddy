import { Component, computed, inject, resource, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { CommonModule } from '@angular/common';

import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { PaginatorModule } from 'primeng/paginator';
import { DialogModule } from 'primeng/dialog';
import { DrawerModule } from 'primeng/drawer';

import { TripsService } from '../../services/trips.service';
import { PAGE_SIZE } from '../../../../shared/constants';
import { TripsApiResponse } from '../../models/trips.models';
import { TripDrawerComponent } from '../../../../components/trip-drawer/trip-drawer.component';
import { Trip } from '../../models/trips.models';

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

    page = signal(1);
    pageSize = PAGE_SIZE;
    skip = computed(() => (this.page() - 1) * PAGE_SIZE);
    tripId: string = '';
    isDeleteDialogVisible = false;
    tripToDeleteId = '';
    isDrawOpen = false;
    selectedTrip = signal<Trip | null>(null);

    trips = resource({
        loader: () => firstValueFrom(this.tripsService.getPaginatedTrips(this.skip(), PAGE_SIZE)),
    });

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
        this.selectedTrip.set(trip || null);
        this.isDrawOpen = true;
    }

    closeDraw() {
        this.isDrawOpen = false;
        this.selectedTrip.set(null);
    }

    onTripSubmit(formValue: any) {
        const editingTrip = this.selectedTrip();
        if (!editingTrip) {
            this.tripsService.createTrip(formValue).subscribe(() => {
                this.trips.reload();
                this.closeDraw();
            });
        } else {
            this.tripsService.updateTrip(editingTrip!.id!, formValue).subscribe(() => {
                this.trips.reload();
                this.closeDraw();
            });
        }
    }
}
