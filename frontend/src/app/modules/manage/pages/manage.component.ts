import { Component, computed, inject, resource, signal } from '@angular/core';
import { TableModule } from 'primeng/table';
import { CompanionsService } from '../services/companions.services';
import { InterestsService } from '../services/interests.services';
import { TravelStylesService } from '../services/travelStyles.services';
import { PAGE_SIZE } from '../../../shared/constants';
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-manage',
  imports: [
    TableModule,
  ],
  templateUrl: './manage.component.html',
  styleUrl: './manage.component.css',
})
export class ManageComponent {
  private readonly companionsService = inject(CompanionsService);
  private readonly interestsService = inject(InterestsService);
  private readonly travelStylesService = inject(TravelStylesService);


  page = signal(1);
  skip = computed(() => (this.page() - 1) * PAGE_SIZE);

  companions = resource({
    loader: () => firstValueFrom(this.companionsService.getPaginatedCompanions(this.skip(), PAGE_SIZE))
  })

  interests = resource({
    loader: () => firstValueFrom(this.interestsService.getPaginatedInterests(this.skip(), PAGE_SIZE))
  })

  travelStyles = resource({
    loader: () => firstValueFrom(this.travelStylesService.getPaginatedTravelStyles(this.skip(), PAGE_SIZE))
  })
}
