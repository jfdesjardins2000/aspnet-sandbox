import { Component, OnDestroy, OnInit } from '@angular/core';
import { AsyncPipe, DatePipe} from '@angular/common';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { MarkdownComponent } from 'ngx-markdown';
import { BlogPostService } from '../../blog-post/services/blog-post.service';
import { BlogPostModel } from '../../blog-post/models/blog-post-model';

@Component({
  selector: 'app-blog-details',
  standalone: true,
  imports: [AsyncPipe, DatePipe, MarkdownComponent],
  templateUrl: './blog-details.component.html',
  styleUrl: './blog-details.component.css'
})
export class BlogDetailsComponent implements OnInit, OnDestroy {

  url: string | null = null;
  paramsSubscription?: Subscription;
  blogPosts$?: Observable<BlogPostModel>;

  constructor(private route: ActivatedRoute, 
    private blogPostService: BlogPostService) {
  }

  ngOnInit(): void {
    this.paramsSubscription = this.route.paramMap.subscribe({
      next: (params:ParamMap) => {
        this.url = params.get('url');

      },
    });

    // Fetch blog details by url
    if (this.url) {
      this.blogPosts$ = this.blogPostService.getBlogPostByUrlHandle(this.url);
      
    }  

  }


  ngOnDestroy(): void {
    this.paramsSubscription?.unsubscribe();
  }

}
