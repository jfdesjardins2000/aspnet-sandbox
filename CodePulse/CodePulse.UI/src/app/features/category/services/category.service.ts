import { Injectable } from '@angular/core';
import { AddCategoryRequestModel } from '../models/add-category-request.model';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(private http: HttpClient ) { }

  addCategory(model: AddCategoryRequestModel): Observable<void>{
    return this.http.post<void>('https://localhost:7015/api/categories/', model);

  }

}
