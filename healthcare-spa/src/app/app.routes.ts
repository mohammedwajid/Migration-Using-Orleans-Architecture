import { Routes } from '@angular/router';
import { EmployeeSearchComponent } from './components/employee-search/employee-search';


export const routes: Routes = [
  { path: '', redirectTo: 'employeesearch', pathMatch: 'full' },
  { path: 'employeesearch', component: EmployeeSearchComponent },

];
