import { Routes } from '@angular/router';
import { Home } from './home/home';
import { About } from './about/about';
import { Contact } from './contact/contact';
import { Login } from './account/login/login';
import { authGuard } from './guards/auth-guard';


export const routes: Routes = [
    { path: '', component: Home },
    { path: 'about', component: About },
    { path: 'contact', component: Contact },
    { path: 'login', component: Login },
    {
        path: 'admin',
        canActivate: [authGuard],
        loadChildren: () => import('./admin/routes')
            .then(m => m.adminRoutes)
    },
    { path: '**', redirectTo: '/' }
];