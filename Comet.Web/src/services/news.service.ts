import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
declare var Bmob: any;

@Injectable()
export class NewsService {
    constructor() {
        Bmob.initialize('75700841fb52cb6a078f861df2a5bbc3', '7d536b0f8e4fcda96529aee305e05439');
    }

    getNewsByPage = (limit, skip): Observable<any> => {
        return new Observable(observer => {
            let news = Bmob.Object.extend("shem_juejin");
            let query = new Bmob.Query(news);
            query.limit(limit);
            query.skip(skip);
            query.descending("viewsCount");
            query.find({
                success: (results) => {
                    let array = [];
                    results.forEach(item => {
                        array.push(item.attributes);
                    });
                    observer.next(array);
                    observer.complete();
                },
                error: (error) => {
                    observer.error(error);
                    observer.complete();
                }
            });
        });
    }
}