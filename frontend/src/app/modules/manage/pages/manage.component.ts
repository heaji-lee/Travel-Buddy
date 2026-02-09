import { Component, computed, inject, resource, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { firstValueFrom } from 'rxjs';

import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { PaginatorModule } from 'primeng/paginator';

import { CompanionsService } from '../services/companions.services';
import { InterestsService } from '../services/interests.services';
import { TravelStylesService } from '../services/travelStyles.services';
import { PAGE_SIZE } from '../../../shared/constants';
import {
    CompanionsApiResponse,
    InterestsApiResponse,
    TravelStylesApiResponse,
} from '../models/manage.models';

@Component({
    selector: 'app-manage',
    imports: [InputTextModule, FormsModule, ButtonModule, PaginatorModule],
    templateUrl: './manage.component.html',
    styleUrl: './manage.component.css',
})
export class ManageComponent {
    private readonly companionsService = inject(CompanionsService);
    private readonly interestsService = inject(InterestsService);
    private readonly travelStylesService = inject(TravelStylesService);

    newCompanionName = '';
    newInterestName = '';
    newTravelStyleName = '';
    pageSize = 5;

    companionsPage = signal(1);
    interestsPage = signal(1);
    travelStylesPage = signal(1);

    companionsSkip = computed(() => (this.companionsPage() - 1) * this.pageSize);
    interestsSkip = computed(() => (this.interestsPage() - 1) * this.pageSize);
    travelStylesSkip = computed(() => (this.travelStylesPage() - 1) * this.pageSize);

    companionsTotalRecords = computed(() =>
        this.companions.hasValue() ? this.companions.value().total : 0,
    );
    interestsTotalRecords = computed(() =>
        this.interests.hasValue() ? this.interests.value().total : 0,
    );
    travelStylesTotalRecords = computed(() =>
        this.travelStyles.hasValue() ? this.travelStyles.value().total : 0,
    );

    companions = resource({
        loader: () =>
            firstValueFrom(
                this.companionsService.getPaginatedCompanions(this.companionsSkip(), this.pageSize),
            ),
    });

    interests = resource({
        loader: () =>
            firstValueFrom(
                this.interestsService.getPaginatedInterests(this.interestsSkip(), this.pageSize),
            ),
    });

    travelStyles = resource({
        loader: () =>
            firstValueFrom(
                this.travelStylesService.getPaginatedTravelStyles(
                    this.travelStylesSkip(),
                    this.pageSize,
                ),
            ),
    });

    createCompanion(name: string) {
        if (!name.trim()) return;

        this.companionsService.createCompanion({ name }).subscribe(() => {
            this.newCompanionName = '';
            this.companions.reload();
        });
    }

    createInterest(name: string) {
        if (!name.trim()) return;

        this.interestsService.createInterest({ name }).subscribe(() => {
            this.newInterestName = '';
            this.interests.reload();
        });
    }

    createTravelStyle(name: string) {
        if (!name.trim()) return;

        this.travelStylesService.createTravelStyle({ name }).subscribe(() => {
            this.newTravelStyleName = '';
            this.travelStyles.reload();
        });
    }

    deleteCompanion(companionId: string) {
        this.companionsService.deleteCompanion(companionId).subscribe(() => {
            this.companions.reload();
        });
    }

    deleteInterest(interestId: string) {
        this.interestsService.deleteInterest(interestId).subscribe(() => {
            this.interests.reload();
        });
    }

    deleteTravelStyle(travelStyleId: string) {
        this.travelStylesService.deleteTravelStyle(travelStyleId).subscribe(() => {
            this.travelStyles.reload();
        });
    }

    onCompanionsPageChange(event: any) {
        this.companionsPage.set(event.page + 1);
        this.companions.reload();
    }

    onInterestsPageChange(event: any) {
        this.interestsPage.set(event.page + 1);
        this.interests.reload();
    }

    onTravelStylesPageChange(event: any) {
        this.travelStylesPage.set(event.page + 1);
        this.travelStyles.reload();
    }
}
