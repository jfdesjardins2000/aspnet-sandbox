import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { AddBlogPostModel } from '../models/add-blog-post.model';
import { BlogPostModel } from '../models/blog-post-model';
import { environment } from '../../../../environments/environment';
import { UpdateBlogPostModel } from '../models/update-blog-post.model';

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

  getBlogPostById(id: string): Observable<BlogPostModel> {
    return this.http.get<BlogPostModel>(`${environment.apiBaseUrl}/api/blogposts/${id}`);
  }

  getBlogPostByUrlHandle(urlHandle: string): Observable<BlogPostModel> {
    return this.http.get<BlogPostModel>(`${environment.apiBaseUrl}/api/blogposts/${urlHandle}`);
  }


  updateBlogPost(id: string, updateBlogPost: UpdateBlogPostModel): Observable<BlogPostModel> {
    return this.http.put<BlogPostModel>(`${environment.apiBaseUrl}/api/blogposts/${id}`, updateBlogPost);
  }

  deleteBlogPost(id: string): Observable<BlogPostModel> {
    return this.http.delete<BlogPostModel>(`${environment.apiBaseUrl}/api/blogposts/${id}`);
  }
}
