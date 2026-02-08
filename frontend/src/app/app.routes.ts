import { Routes } from '@angular/router';
import { TripsControlComponent } from './modules/trips/pages/trips-control/trips-control.component';
import { TripsListComponent } from './modules/trips/pages/trips-list/trips-list.component';
import { ManageComponent } from './modules/manage/pages/manage.component';

export const routes: Routes = [
    { path: '', component: TripsListComponent },
    { path: 'home', component: TripsListComponent },
    // { path: 'trips/new', component: TripsControlComponent },
    // { path: 'trips/:id', component: TripsControlComponent },
    { path: 'manage', component: ManageComponent },
];
