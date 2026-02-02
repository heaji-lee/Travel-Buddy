import { Routes } from '@angular/router';
import { HomeComponent } from './modules/home/pages/home.component';
import { TripsControlComponent } from './modules/trips/pages/trips-control/trips-control.component';
import { TripsListComponent } from './modules/trips/pages/trips-list/trips-list.component';
import { ManageComponent } from './modules/manage/manage.component';

export const routes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'home', component: HomeComponent },
    { path: 'trips/new', component: TripsControlComponent },
    { path: 'trips/:id', component: TripsControlComponent },
    { path: 'trips', component: TripsListComponent },
    { path: 'manage', component: ManageComponent },
];
