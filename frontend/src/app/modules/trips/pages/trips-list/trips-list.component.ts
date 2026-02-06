import { Component, computed, inject, resource, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { PaginatorModule, PaginatorState } from 'primeng/paginator';
import { DialogModule } from 'primeng/dialog';

import { TripsService } from '../../services/trips.service';
import { PAGE_SIZE } from '../../../../shared/constants';
import { TripsApiResponse } from '../../models/trips.models';

@Component({
    selector: 'app-trips-list',
    imports: [TableModule, CommonModule, ButtonModule, RouterLink, PaginatorModule, DialogModule],
    templateUrl: './trips-list.component.html',
    styleUrl: './trips-list.component.css',
})
export class TripsListComponent {
    private readonly tripsService = inject(TripsService);

    page = signal(1);
    pageSize = PAGE_SIZE;
    skip = computed(() => (this.page() - 1) * PAGE_SIZE);
    tripId: string = '';
    isDialogVisible = false;

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

    showDeleteDialog(tripId: string) {
        this.isDialogVisible = true;
    }
}
