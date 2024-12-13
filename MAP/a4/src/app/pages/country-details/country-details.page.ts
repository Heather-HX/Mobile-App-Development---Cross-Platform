import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Preferences } from '@capacitor/preferences';
import { ToastController } from '@ionic/angular';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-country-details',
  templateUrl: './country-details.page.html',
  styleUrls: ['./country-details.page.scss'],
})
export class CountryDetailsPage implements OnInit, OnDestroy {
  country: any = {};
  private routeSub!: Subscription;

  constructor(
    private route: ActivatedRoute,
    private http: HttpClient,
    private toastController: ToastController
  ) {}

  ngOnInit() {
    this.routeSub = this.route.queryParams.subscribe((params) => {
      const alpha3Code = params['code']; //
      if (alpha3Code) {
        this.loadCountryDetails(alpha3Code);
      }
    });
  }

  loadCountryDetails(alpha3Code: string) {
    this.http
      .get(`https://restcountries.com/v2/alpha/${alpha3Code}`)
      .subscribe((data) => {
        this.country = data;
      });
  }

  async addToFavorites() {
    const { value } = await Preferences.get({ key: 'favorites' });
    const favorites = value ? JSON.parse(value) : [];

    if (!favorites.includes(this.country.alpha3Code)) {
      favorites.push(this.country.alpha3Code);
      await Preferences.set({
        key: 'favorites',
        value: JSON.stringify(favorites),
      });

      const toast = await this.toastController.create({
        message: 'Added to Favorites!',
        duration: 2000,
        color: 'success',
      });
      toast.present();
    } else {
      const toast = await this.toastController.create({
        message: 'Already in Favorites!',
        duration: 2000,
        color: 'warning',
      });
      toast.present();
    }
  }

  ngOnDestroy() {
    if (this.routeSub) {
      this.routeSub.unsubscribe();
    }
  }
}
