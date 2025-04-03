import { Component, OnInit } from '@angular/core';
import { AsyncPipe } from '@angular/common';
import { RouterLink } from '@angular/router';
import { BlogPostService } from '../../blog-post/services/blog-post.service';
import { Observable } from 'rxjs';
import { BlogPostModel } from '../../blog-post/models/blog-post-model';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [AsyncPipe, RouterLink],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {

  blogs$?: Observable<BlogPostModel[]>;

  constructor(private blogPostService:BlogPostService) { 
  }

  ngOnInit(): void {
    this.blogs$ = this.blogPostService.getAllBlogPosts();
  }

}
