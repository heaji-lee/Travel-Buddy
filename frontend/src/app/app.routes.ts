import { Routes } from '@angular/router';
import { TripsListComponent } from './modules/trips/pages/trips-list/trips-list.component';
import { ManageComponent } from './modules/manage/pages/manage.component';
import { LoginComponent } from './modules/login/pages/login/login.component';
import { AskComponent } from './modules/ask/pages/ask.component';

export const routes: Routes = [
    { path: 'login', component: LoginComponent },
    {
        path: '',
        children: [
            { path: '', component: TripsListComponent },
            { path: 'home', component: TripsListComponent },
            { path: 'manage', component: ManageComponent },
            { path: 'ask', component: AskComponent },
        ],
    },
    { path: '**', redirectTo: '' },
];
