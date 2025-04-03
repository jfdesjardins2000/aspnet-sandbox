import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { BlogImageModel } from '../models/blog-image.model';
import { HttpClient } from '@angular/common/http';
import { environment } from './../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ImageService {

  selectedImage: BehaviorSubject<BlogImageModel> = new BehaviorSubject<BlogImageModel>({
    id: '',
    fileExtenstion: '',
    fileName: '',
    title: '',
    url: ''
  });

  constructor(private http: HttpClient) { }

  getAllImages(): Observable<BlogImageModel[]> {
    return this.http.get<BlogImageModel[]>(`${environment.apiBaseUrl}/api/images`);
  }

  uploadImage(file: File, fileName: string, title: string): Observable<BlogImageModel> {
    const formData = new FormData();
    formData.append('file', file);
    formData.append('fileName', fileName);
    formData.append('title', title);
    
    return this.http.post<BlogImageModel>(`${environment.apiBaseUrl}/api/images`, formData);
  }

  selectImage(image: BlogImageModel): void {
    this.selectedImage.next(image);
  }

  onSelectImage(): Observable<BlogImageModel> {
    return this.selectedImage.asObservable()
  }

}
