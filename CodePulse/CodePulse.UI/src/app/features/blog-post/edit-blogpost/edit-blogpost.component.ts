import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { BlogPostModel } from '../models/blog-post-model';
import { BlogPostService } from '../services/blog-post.service';
import { FormsModule } from '@angular/forms';
import { AsyncPipe, DatePipe } from '@angular/common';
import { MarkdownComponent } from 'ngx-markdown';

@Component({
  selector: 'app-edit-blogpost',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './edit-blogpost.component.html',
  styleUrl: './edit-blogpost.component.css',
})
export class EditBlogpostComponent implements OnInit, OnDestroy {
  id: string | null = null;
  model?: BlogPostModel;
  paramsSubscription?: Subscription;
  

  isImageSelectorVisible : boolean = false;

  constructor(
    private route: ActivatedRoute,
    private blogPostService: BlogPostService,
  ) {}

  ngOnInit(): void {
    this.paramsSubscription = this.route.paramMap.subscribe({
      next: (params) => {
        this.id = params.get('id');

        if (this.id) {
          // get the data from the API for this category Id
          this.blogPostService.getBlogPostById(this.id).subscribe({
            next: (response) => {
              this.model = response;
            },
          });
        }
      },
      error: (err) => {
        console.error(err);
      },
    });
  }

  onFormSubmit() {
    throw new Error('Method not implemented.');
  }


  openImageSelector(): void {
    this.isImageSelectorVisible = true;
  }

  closeImageSelector() : void {
    this.isImageSelectorVisible = false;
  }


  ngOnDestroy(): void {
    this.paramsSubscription?.unsubscribe();
  }
}
