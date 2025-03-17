import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { DatePipe } from '@angular/common';
import { BlogPostService } from '../services/blog-post.service';
import { AddBlogPostModel } from '../models/add-blog-post.model';
import { Router } from '@angular/router';
import { MarkdownComponent } from 'ngx-markdown'; // Importez MarkdownComponent
import { CategoryModel } from '../../category/models/category.model';
import { Observable, Subscription } from 'rxjs';
import { CategoryService } from '../../category/services/category.service';


@Component({
  selector: 'app-add-blogpost',
  standalone: true,
  imports: [FormsModule, DatePipe, MarkdownComponent],
  templateUrl: './add-blogpost.component.html',
  styleUrl: './add-blogpost.component.css'
})
export class AddBlogpostComponent implements OnInit, OnDestroy {

  model: AddBlogPostModel
  isImageSelectorVisible : boolean = false;
  categories$?: Observable<CategoryModel[]>;
  imageSelectorSubscription?: Subscription;

  constructor(private blogPostService: BlogPostService, 
    private router: Router,
    private categoryService: CategoryService) {
    this.model = {
      title: '',
      shortDescription: '',
      urlHandle: '',
      content: '',
      featuredImageUrl: '',
      author: '',
      isVisible: true,
      publishedDate: new Date(),
      categories: []
    }
    
  }

  ngOnInit(): void {
    console.log("ngOnInit");
    this.categories$ = this.categoryService.getAllCategories();

    // this.imageSelectorSubscription = this.imageService.onSelectImage()
    // .subscribe({
    //  next: (selectedImage) => {
    //    this.model.featuredImageUrl = selectedImage.url;
    //    this.closeImageSelector();
    //  }
    // })
  }

  onFormSubmit():void{
    console.log(this.model);
    this.blogPostService.createBlogPost(this.model)
      .subscribe({
        next: (response) => {
            console.log("Add BlogPost Successfull!");
            this.router.navigateByUrl('/admin/blogposts')
        },
        error: (err) => {
          console.error(err);
        }
      });
  }

  
  openImageSelector(): void {
    this.isImageSelectorVisible = true;
  }

  closeImageSelector() : void {
    this.isImageSelectorVisible = false;
  }

  ngOnDestroy(): void {
    // throw new Error('Method not implemented.');
    console.log("ngOnDestroy");
  }

}
