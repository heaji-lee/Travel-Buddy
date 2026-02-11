import { Routes } from '@angular/router';
import { TripsListComponent } from './modules/trips/pages/trips-list/trips-list.component';
import { ManageComponent } from './modules/manage/pages/manage.component';
import { WeatherComponent } from './modules/weather/pages/weather.component';

export const routes: Routes = [
    { path: '', component: TripsListComponent },
    { path: 'home', component: TripsListComponent },
    { path: 'manage', component: ManageComponent },
    { path: 'weather', component: WeatherComponent },
];
