import { Component, computed, inject, resource, signal } from '@angular/core';
import { TableModule } from 'primeng/table';
import { ManageService } from '../services/manage.services';
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
  private readonly manageService = inject(ManageService);

  page = signal(1);
  skip = computed(() => (this.page() - 1) * PAGE_SIZE);

  companions = resource({
    loader: () => firstValueFrom(this.manageService.getPaginatedCompanions(this.skip(), PAGE_SIZE))
  })

  interests = resource({
    loader: () => firstValueFrom(this.manageService.getPaginatedInterests(this.skip(), PAGE_SIZE))
  })

  travelStyles = resource({
    loader: () => firstValueFrom(this.manageService.getPaginatedTravelStyles(this.skip(), PAGE_SIZE))
  })
}
