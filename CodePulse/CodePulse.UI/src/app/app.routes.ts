import { Routes } from '@angular/router';
import { HomeComponent } from './features/public/home/home.component';
import { CategoryListComponent } from './features/category/category-list/category-list.component';
import { AddCategoryComponent } from './features/category/add-category/add-category.component';
import { EditCategoryComponent } from './features/category/edit-category/edit-category.component';
import { BlogpostListComponent } from './features/blog-post/blogpost-list/blogpost-list.component';
import { AddBlogpostComponent } from './features/blog-post/add-blogpost/add-blogpost.component';
import { EditBlogpostComponent } from './features/blog-post/edit-blogpost/edit-blogpost.component';
import { BlogDetailsComponent } from './features/public/blog-details/blog-details.component';

export const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
  },
  {
    path: 'admin/categories',
    component: CategoryListComponent,
  },
  {
    path: 'blog/:url',
    component: BlogDetailsComponent,
  },
  {
    path: 'admin/categories/add',
    component: AddCategoryComponent,
  },
  {
    path: 'admin/categories/:id',
    component: EditCategoryComponent,
  },
  {
    path: 'admin/blogposts',
    component: BlogpostListComponent,
  },
  {
    path: 'admin/blogposts/add',
    component: AddBlogpostComponent,
  },
  {
    path: 'admin/blogposts/:id',
    component: EditBlogpostComponent,
  },
];

// import { HomeComponent } from './features/public/home/home.component';
// import { BlogDetailsComponent } from './features/public/blog-details/blog-details.component';
// import { LoginComponent } from './features/auth/login/login.component';
// import { authGuard } from './features/auth/guards/auth.guard';

// export const routes: Routes = [
//     {
//       path: 'login',
//       component: LoginComponent
//     },
// ];
