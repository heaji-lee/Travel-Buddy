import { Routes } from '@angular/router';
import { Home } from './pages/home/home';
import { NewTrip } from './pages/new-trip/new-trip';
import { AllTrips } from './pages/all-trips/all-trips';

export const routes: Routes = [
  { path: '', component: Home },
  { path: 'home', component: Home },
  { path: 'new-trip', component: NewTrip },
  { path: 'all-trips', component: AllTrips },
];
