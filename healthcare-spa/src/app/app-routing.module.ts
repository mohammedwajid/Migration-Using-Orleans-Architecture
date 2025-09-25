import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EmployeeSearchComponent } from './components/employee-search/employee-search';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';


const routes: Routes = [
  { path: '', redirectTo: '/book', pathMatch: 'full' },
  { path: 'employeesearch', component: EmployeeSearchComponent },

];

@NgModule({
  imports: [
    RouterModule.forRoot(routes),
    FormsModule,
     BrowserModule,
    HttpClientModule  
  
  ],
  exports: [RouterModule]
})
export class AppRoutingModule {}
