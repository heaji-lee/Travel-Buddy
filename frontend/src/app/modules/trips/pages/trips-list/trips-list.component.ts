import { Component, computed, inject, resource, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { TripsService } from '../../services/trips.service';
import { PAGE_SIZE } from '../../../../shared/constants';
import { TripsApiResponse } from '../../models/trips.models';

@Component({
    selector: 'app-trips-list',
    imports: [TableModule, CommonModule, ButtonModule, RouterLink],
    templateUrl: './trips-list.component.html',
    styleUrl: './trips-list.component.css',
})
export class TripsListComponent {
    private readonly tripsService = inject(TripsService);

    page = signal(1);
    skip = computed(() => (this.page() - 1) * PAGE_SIZE);
    tripId: string = '';

    trips = resource({
      loader: () => firstValueFrom(
        this.tripsService.getPaginatedTrips(this.skip(), PAGE_SIZE)
      )
    })

    totalRecords = computed(() => {
      const value = this.trips.value();
      return (value as any as TripsApiResponse)?.total || 0;
    })

    goToNextPage() {
        const maxPage = Math.ceil(this.totalRecords() / PAGE_SIZE);
        this.page.update((p) => (p < maxPage ? p + 1 : p));
    }

    goToPreviousPage() {
        this.page.update((p) => (p > 1 ? p - 1 : p));
    }

    deleteTrip(tripId: string) {
      this.tripsService.deleteTrip(tripId).subscribe(() => {
        this.trips.reload();
      });
    }
}
