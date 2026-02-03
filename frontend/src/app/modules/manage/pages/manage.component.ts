import { Component, computed, inject, resource, signal } from '@angular/core';
import { CompanionsService } from '../services/companions.services';
import { InterestsService } from '../services/interests.services';
import { TravelStylesService } from '../services/travelStyles.services';
import { PAGE_SIZE } from '../../../shared/constants';
import { firstValueFrom } from 'rxjs';
import { InputTextModule } from 'primeng/inputtext';
import { FormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';

@Component({
  selector: 'app-manage',
  imports: [InputTextModule, FormsModule, ButtonModule],
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

  page = signal(1);
  pageSize = 5;
  skip = computed(() => (this.page() - 1) * this.pageSize);

  companions = resource({
    loader: () => firstValueFrom(this.companionsService.getPaginatedCompanions(this.skip(), this.pageSize))
  })

  interests = resource({
    loader: () => firstValueFrom(this.interestsService.getPaginatedInterests(this.skip(), this.pageSize))
  })

  travelStyles = resource({
    loader: () => firstValueFrom(this.travelStylesService.getPaginatedTravelStyles(this.skip(), this.pageSize))
  })

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

}
