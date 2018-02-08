import { Component } from '@angular/core';
import { NavController } from 'ionic-angular';
import { NewsService } from '../../services/news.service';

@Component({
  selector: 'page-home',
  templateUrl: 'home.html',
  providers: [NewsService]
})
export class HomePage {

  news = [];
  limit = 20;
  skip = 0;
  constructor(
    public navCtrl: NavController,
    private _nSer: NewsService
  ) {

  }
  ngOnInit() {
    this._nSer.getNewsByPage(this.limit, this.skip).subscribe(data => {
      this.news = data;
    });
  }
  doRefresh(refresher) {
    this.skip = 0;
    this._nSer.getNewsByPage(this.limit, this.skip).subscribe(data => {
      this.news = data;
      refresher.complete();
    });
  }
  doInfinite(infiniteScroll) {
    this.skip += this.limit;
    this._nSer.getNewsByPage(this.limit, this.skip).subscribe(data => {
      data.forEach(item => {
        this.news.push(item);
      });
      infiniteScroll.complete();
      if (data.length === 0) {
        infiniteScroll.enable(false);
      }
    });
  }
}
