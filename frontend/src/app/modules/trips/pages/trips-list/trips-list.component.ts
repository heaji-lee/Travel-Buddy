import { Component, computed, inject, resource, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { CommonModule } from '@angular/common';

import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { PaginatorModule } from 'primeng/paginator';
import { DialogModule } from 'primeng/dialog';
import { Drawer, DrawerModule } from 'primeng/drawer';

import { TripsService } from '../../services/trips.service';
import { PAGE_SIZE } from '../../../../shared/constants';
import { TripsApiResponse } from '../../models/trips.models';
import { TripDrawerComponent } from '../../../../components/trip-drawer/trip-drawer.component'

@Component({
    selector: 'app-trips-list',
    imports: [TableModule, CommonModule, ButtonModule, PaginatorModule, DialogModule, Drawer, DrawerModule, TripDrawerComponent],
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
    isDrawOpened = false;
    selectedTrip: any = null;

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
        this.tripsService.deleteTrip(tripId).subscribe(() => {
            this.trips.reload();
        });
    }

    showDeleteDialog() {
        this.isDeleteDialogVisible = true;
    }

    showDraw(trip: any) {
      this.selectedTrip = trip;
      this.isDrawOpened = true;
    }
}
