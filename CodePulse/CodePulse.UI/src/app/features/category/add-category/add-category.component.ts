import { Component, OnDestroy } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AddCategoryRequestModel } from '../models/add-category-request.model';
import { CategoryService } from '../services/category.service';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-category',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './add-category.component.html',
  styleUrl: './add-category.component.css'
})
export class AddCategoryComponent implements OnDestroy {
  
  private addCategorySubscription?: Subscription
  
  // model: {
  //   name: string;
  //   urlHandle: string;
  // } = {
  //   name: '',
  //   urlHandle: ''
  // };
  model: AddCategoryRequestModel;

  constructor(private categoryService: CategoryService, private router: Router) {
    this.model = {name: '', urlHandle: ''};   
  }

  onFormSubmit() {
    // Votre logique de soumission ici
    console.log(`name: ${this.model.name} urlHandle:${this.model.urlHandle}`);
    
    this.addCategorySubscription = this.categoryService.addCategory(this.model)
    .subscribe({
      next: (response) => {
          console.log("Add Category Successfull!");
          this.router.navigateByUrl('/admin/categories')
      },
      error: (err) => {
        console.error(err);
      }
    });
  }
  
  ngOnDestroy(): void {
    console.log('addCategorySubscription.unsubscribe');
    this.addCategorySubscription?.unsubscribe();
  }
}
