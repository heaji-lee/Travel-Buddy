import { Routes } from '@angular/router';
import { Home } from './modules/home/pages/home';
import { NewTrip } from './modules/new-trip/pages/new-trip';
import { AllTrips } from './modules/all-trips/pages/all-trips';
import { Manage } from './modules/manage/manage';

export const routes: Routes = [
    { path: '', component: Home },
    { path: 'home', component: Home },
    { path: 'new-trip', component: NewTrip },
    { path: 'all-trips', component: AllTrips },
    { path: 'manage', component: Manage },
];
