import { Component, OnInit } from '@angular/core';
import { Preferences } from '@capacitor/preferences';

@Component({
  selector: 'app-favorites',
  templateUrl: './favorites.page.html',
  styleUrls: ['./favorites.page.scss'],
})
export class FavoritesPage implements OnInit {
  favorites: string[] = [];

  async ngOnInit() {
    await this.loadFavorites();
  }

  async ionViewWillEnter() {
    // reload
    await this.loadFavorites();
  }

  async loadFavorites() {
    const { value } = await Preferences.get({ key: 'favorites' });
    this.favorites = value ? JSON.parse(value) : [];
  }

  async removeFavorite(code: string) {
    this.favorites = this.favorites.filter((fav) => fav !== code);
    await Preferences.set({
      key: 'favorites',
      value: JSON.stringify(this.favorites),
    });
  }

  async clearFavorites() {
    this.favorites = [];
    await Preferences.remove({ key: 'favorites' });
  }
}
