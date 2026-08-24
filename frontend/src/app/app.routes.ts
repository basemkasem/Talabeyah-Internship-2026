import { Routes } from '@angular/router';
import { Login } from './features/auth/pages/login/login';
import { ProductsList } from './features/products/pages/products-list/products-list';
import { MainLayout } from './shared/components/main-layout/main-layout';

export const routes: Routes = [
  {
    path: 'login',
    component: Login,
  },
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
    ],
  },
];
