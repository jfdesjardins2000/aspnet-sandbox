import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { AddBlogPostModel } from '../models/add-blog-post.model';
import { BlogPostModel } from '../models/blog-post-model';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class BlogPostService {

  constructor(private http: HttpClient) { }

  createBlogPost(data: AddBlogPostModel) : Observable<BlogPostModel> {
    return this.http.post<BlogPostModel>(`${environment.apiBaseUrl}/api/blogposts`, data);
  }

  getAllBlogPosts() : Observable<BlogPostModel[]> {
    return this.http.get<BlogPostModel[]>(`${environment.apiBaseUrl}/api/blogposts`);
  }

}
