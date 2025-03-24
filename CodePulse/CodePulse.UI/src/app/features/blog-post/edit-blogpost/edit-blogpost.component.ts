import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { BlogPostModel } from '../models/blog-post-model';
import { BlogPostService } from '../services/blog-post.service';
import { FormsModule } from '@angular/forms';
import { AsyncPipe, DatePipe } from '@angular/common';
import { MarkdownComponent } from 'ngx-markdown';
import { CategoryService } from '../../category/services/category.service';
import { CategoryModel } from '../../category/models/category.model';

@Component({
  selector: 'app-edit-blogpost',
  standalone: true,
  imports: [FormsModule, MarkdownComponent, AsyncPipe, DatePipe],
  templateUrl: './edit-blogpost.component.html',
  styleUrl: './edit-blogpost.component.css',
})
export class EditBlogpostComponent implements OnInit, OnDestroy {
  
  id: string | null = null;
  model?: BlogPostModel;
  categories$?: Observable<CategoryModel[]>;
  selectedCategories?: string[];
  isImageSelectorVisible : boolean = false;

  routeSubscription?: Subscription;
  updateBlogPostSubscription?: Subscription;
  getBlogPostSubscription?: Subscription;
  deleteBlogPostSubscription?: Subscription;
  imageSelectSubscricption?: Subscription;

  constructor(
    private route: ActivatedRoute,
    private blogPostService: BlogPostService,
    private categoryService: CategoryService,
    private router:Router
  ) {}

  ngOnInit(): void {

    this.categories$ = this .categoryService.getAllCategories();

    this.routeSubscription = this.route.paramMap.subscribe({
      next: (params) => {
        this.id = params.get('id');

        // Get BlogPost From API
        if (this.id) {
          // get the data from the API for this category Id
          this.getBlogPostSubscription =  this.blogPostService.getBlogPostById(this.id).subscribe({
            next: (response) => {
              this.model = response;
              this.selectedCategories = response.categories.map(x => x.id )
            }
          });
        }

        //this.imageSelectSubscricption = this.ima

      },
      error: (err) => {
        console.error(err);
      },
    });
  }

  onFormSubmit(): void {

    console.log("onFormSubmit");
    console.log(this.model);
  }


  openImageSelector(): void {
    this.isImageSelectorVisible = true;
  }

  closeImageSelector() : void {
    this.isImageSelectorVisible = false;
  }


  ngOnDestroy(): void {
    this.routeSubscription?.unsubscribe();
  }
}
