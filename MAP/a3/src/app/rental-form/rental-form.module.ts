import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { RentalFormPageRoutingModule } from './rental-form-routing.module';

import { RentalFormPage } from './rental-form.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    RentalFormPageRoutingModule
  ],
  declarations: [RentalFormPage]
})
export class RentalFormPageModule {}
