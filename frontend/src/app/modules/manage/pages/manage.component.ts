import { Component, computed, inject, resource, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { firstValueFrom } from 'rxjs';

import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { PaginatorModule } from 'primeng/paginator';

import { CompanionsService } from '../services/companions.services';
import { InterestsService } from '../services/interests.services';
import { TravelStylesService } from '../services/travelStyles.services';

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

    companionsTotalPages = computed(() => Math.ceil(this.companionsTotalRecords() / this.pageSize));
    interestsTotalPages = computed(() => Math.ceil(this.interestsTotalRecords() / this.pageSize));
    travelStylesTotalPages = computed(() =>
        Math.ceil(this.travelStylesTotalRecords() / this.pageSize),
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

    prevCompanions() {
        if (this.companionsPage() > 1) {
            this.companionsPage.set(this.companionsPage() - 1);
            this.companions.reload();
        }
    }

    nextCompanions() {
        if (!this.isCompanionsLastPage()) {
            this.companionsPage.update((p) => p + 1);
            this.companions.reload();
        }
    }

    isCompanionsFirstPage() {
        return this.companionsPage() === 1;
    }

    isCompanionsLastPage() {
        return this.companionsPage() >= this.companionsTotalPages();
    }

    prevInterests() {
        if (this.interestsPage() > 1) {
            this.interestsPage.set(this.interestsPage() - 1);
            this.interests.reload();
        }
    }

    nextInterests() {
        if (!this.isInterestsLastPage()) {
            this.interestsPage.update((p) => p + 1);
            this.interests.reload();
        }
    }

    isInterestsFirstPage() {
        return this.interestsPage() === 1;
    }

    isInterestsLastPage() {
        return this.interestsPage() >= this.interestsTotalPages();
    }

    prevTravelStyles() {
        if (this.travelStylesPage() > 1) {
            this.travelStylesPage.set(this.travelStylesPage() - 1);
            this.travelStyles.reload();
        }
    }

    nextTravelStyles() {
        if (!this.isTravelStylesLastPage()) {
            this.travelStylesPage.update((p) => p + 1);
            this.travelStyles.reload();
        }
    }

    isTravelStylesFirstPage() {
        return this.travelStylesPage() === 1;
    }

    isTravelStylesLastPage() {
        return this.travelStylesPage() >= this.travelStylesTotalPages();
    }
}
