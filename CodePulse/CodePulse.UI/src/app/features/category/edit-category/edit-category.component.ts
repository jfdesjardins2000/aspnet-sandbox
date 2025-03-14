import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { CategoryModel } from '../models/category.model';
import { CategoryService } from '../services/category.service';
import { UpdateCategoryRequestModel } from '../models/update-category-request.model';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-edit-category',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './edit-category.component.html',
  styleUrl: './edit-category.component.css'
})
export class EditCategoryComponent implements OnInit, OnDestroy {
  
  id: string | null = null;
  paramsSubscription?: Subscription;
  editCategorySubscription?: Subscription;
  category?: CategoryModel;

  /**
   * Constructeur
   */
  constructor(private route: ActivatedRoute, 
    private categoryService: CategoryService,
    private router: Router) {
  }
  
  ngOnInit(): void {
    this.paramsSubscription = this.route.paramMap.subscribe({
      next: (params) => {
          this.id = params.get('id');

          if (this.id) {
            // get the data from the API for this category Id
            this.categoryService.getCategoryById(this.id)
            .subscribe({
              next: (response) => {
                this.category = response;
              }
            });           
          }
      },
      error: (err) => {
        console.error(err);
      }
    });
  }

  onFormSubmit(): void {

    console.log(this.category);

    const updateCategoryRequestModel: UpdateCategoryRequestModel = {
      name: this.category?.name ?? '',
      urlHandle: this.category?.urlHandle ?? ''
    };

    // pass this object to service
    if (this.id) {
      this.editCategorySubscription = this.categoryService.updateCategory(this.id, updateCategoryRequestModel)
      .subscribe({
        next: (response) => {
          this.router.navigateByUrl('/admin/categories');
        }
      });
    }
  }

  onDelete(): void {
    if (this.id) {
      this.categoryService.deleteCategory(this.id)
      .subscribe({
        next: (response) => {
          this.router.navigateByUrl('/admin/categories');
        }
      })
    }
  }  
  
  ngOnDestroy(): void {
    this.paramsSubscription?.unsubscribe();
  }
    
}
