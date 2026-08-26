import { Routes } from '@angular/router';
import { MainLayout } from './shared/components/main-layout/main-layout';

export const routes: Routes = [
  {
    path: '',
    component: MainLayout,
    children: [
      {
        path: '',
        redirectTo: 'products',
        pathMatch: 'full',
      },
      {
        path: 'products',
        loadComponent: () =>
          import('./features/products/pages/products-list/products-list').then(
            (m) => m.ProductsList,
          ),
      },
      {
        path: 'login',
        loadComponent: () =>
          import('./features/auth/pages/login/login')
            .then(m => m.Login)
      }
    ],
  },
];
