import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: '', redirectTo: 'rental-form', pathMatch: 'full' },
  { path: 'rental-form', loadChildren: () => import('./rental-form/rental-form.module').then(m => m.RentalFormPageModule) },
  { path: 'receipt', loadChildren: () => import('./receipt/receipt.module').then(m => m.ReceiptPageModule) }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
