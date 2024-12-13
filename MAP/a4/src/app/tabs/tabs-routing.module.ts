import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TabsPage } from './tabs.page';

const routes: Routes = [
  {
    path: '',
    component: TabsPage,
    children: [
      {
        path: 'countries',
        loadChildren: () =>
          import('../pages/countries/countries.module').then(
            (m) => m.CountriesPageModule
          ),
      },
      {
        path: 'country-details',
        loadChildren: () =>
          import('../pages/country-details/country-details.module').then(
            (m) => m.CountryDetailsPageModule
          ),
      },
      {
        path: 'favorites',
        loadChildren: () =>
          import('../pages/favorites/favorites.module').then(
            (m) => m.FavoritesPageModule
          ),
      },
      {
        path: '',
        redirectTo: '/tabs/countries',
        pathMatch: 'full',
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class TabsPageRoutingModule {}
