import { Injectable } from '@angular/core';
import { AddCategoryRequestModel } from '../models/add-category-request.model';
import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { CategoryModel } from '../models/category.model'; 
import { UpdateCategoryRequestModel } from '../models/update-category-request.model';
import { environment } from '../../../../environments/environment';


@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(private http: HttpClient ) { }

  getAllCategories(
    query?: string, sortBy?: string, sortDirection?: string,
    pageNumber?: number, pageSize?: number): Observable<CategoryModel[]> {
    let params = new HttpParams();

    if (query) {
      params = params.set('query', query)
    }

    if (sortBy) {
      params = params.set('sortBy', sortBy)
    }

    if (sortDirection) {
      params = params.set('sortDirection', sortDirection)
    }

    if (pageNumber) {
      params = params.set('pageNumber', pageNumber)
    }

    if (pageSize) {
      params = params.set('pageSize', pageSize)
    }

    return this.http.get<CategoryModel[]>(`${environment.apiBaseUrl}/api/categories`, {params: params});
  }

  getCategoryById(id: string): Observable<CategoryModel> {
    return this.http.get<CategoryModel>(`${environment.apiBaseUrl}/api/categories/${id}`);
  }

  getCategoryCount(): Observable<number> {
    return this.http.get<number>(`${environment.apiBaseUrl}/api/categories/count`);
  }

  addCategory(model: AddCategoryRequestModel): Observable<void>{
    // return this.http.post<void>(`${environment.apiBaseUrl}/api/categories?addAuth=true`, model);
    return this.http.post<void>(`${environment.apiBaseUrl}/api/categories`, model);
  }

  updateCategory(id: string, updateCategoryRequest: UpdateCategoryRequestModel): Observable<CategoryModel> {
    // return this.http.put<CategoryModel>(`${environment.apiBaseUrl}/api/categories/${id}?addAuth=true`, updateCategoryRequest);
    return this.http.put<CategoryModel>(`${environment.apiBaseUrl}/api/categories/${id}`, updateCategoryRequest);
  }

  deleteCategory(id: string): Observable<CategoryModel> {
    // return this.http.delete<CategoryModel>(`${environment.apiBaseUrl}/api/categories/${id}?addAuth=true`)
    return this.http.delete<CategoryModel>(`${environment.apiBaseUrl}/api/categories/${id}`)
  }
}
