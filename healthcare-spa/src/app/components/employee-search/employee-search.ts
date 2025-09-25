import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { searchcriteria } from '../../models/notification.model';
import { DynamicGrainService, DynamicGrainRequest } from '../../services/dynamic-grain.service';


@Component({
  selector: 'app-employee-search',
  standalone: true,
  imports: [CommonModule, FormsModule],
  template: `
    <div class="search-container">
      <h3>Enter your Employee search criteria:</h3>

      <form (ngSubmit)="searchEmployee()" #searchForm="ngForm">
        <div class="row">
          <div class="col">
            <label for="lastname">Last Name</label>
            <input id="lastname" type="text" [(ngModel)]="lastname" name="lastname" />
          </div>
          <div class="col">
            <label for="code">Code</label>
            <input id="code" type="text" [(ngModel)]="code" name="code" />
          </div>
        </div>

        <div class="row full-width">
          <label for="firstname">First Name</label>
          <input id="firstname" type="text" [(ngModel)]="firstname" name="firstname" />
        </div>

        <button type="submit">Search</button>
      </form>

      <div *ngIf="this.employees" class="result-container">
        <h4>Result:</h4>
        <pre>{{ this.employees | json }}</pre>
      </div>
    </div>

   <div class="results-container">
  <div class="results-header">
    <span>{{ employees.length }} items were found.</span>
  </div>

  <div class="table-wrapper">
    <table class="results-table">
      <thead>
        <tr>
          <th>Code</th>
          <th>First Name</th>
          <th>Last Name</th>
        </tr>
      </thead>
      <tbody>

      <!-- @for (emp of this.employees; track emp.code) {
      <tr>
      <td>{{ emp.code }}</td>
                <td>{{ emp.firstName }}</td>
                <td>{{ emp.lastName }}</td>
        </tr>
      } -->
      </tbody>
    </table>
  </div>
</div>


  `,
  styles: [`
    .search-container {
      max-width: 600px;
      margin: 20px auto;
      padding: 15px;
      border: 1px solid #ccc;
      border-radius: 6px;
      background-color: #f9f9f9;
      font-family: Arial, sans-serif;
    }
    .row {
      display: flex;
      gap: 10px;
      margin-bottom: 15px;
    }
    .col {
      flex: 1;
      display: flex;
      flex-direction: column;
    }
    .full-width {
      display: flex;
      flex-direction: column;
      margin-bottom: 15px;
    }
    label {
      font-weight: bold;
      margin-bottom: 5px;
    }
    input {
      padding: 6px 8px;
      border-radius: 4px;
      border: 1px solid #ccc;
      width: 100%;
    }
    button {
      padding: 8px 16px;
      background-color: #007bff;
      color: white;
      border: none;
      border-radius: 4px;
      font-weight: bold;
      cursor: pointer;
    }
    button:hover {
      background-color: #0056b3;
    }
    .result-container {
      margin-top: 20px;
      padding: 10px;
      background-color: #eef;
      border-radius: 4px;
      border: 1px solid #99c;
    }

    .results-container {
  background: #f5f9ff;
  padding: 12px;
  border-radius: 8px;
}

.results-header {
  margin-bottom: 10px;
  display: flex;
  gap: 10px;
  align-items: center;
}

.results-table {
  width: 100%;
  border-collapse: collapse;
}

.results-table th,
.results-table td {
  border: 1px solid #ddd;
  padding: 6px 10px;
  text-align: left;
}

.results-table th {
  background-color: #e6f0ff;
  font-weight: bold;
}



  `]
})
export class EmployeeSearchComponent {
  firstname = '';
  lastname = '';
  code = '';
  result: any;
 employees: searchcriteria[] = [];
  constructor(private dynamicService: DynamicGrainService) { }

  trackByCode(index: number, emp: any): string {
  return emp.code;
}

  searchEmployee() {
    const request: DynamicGrainRequest = {
      grainType: 'employee',
      grainKey: 'search',
      parameters: {
        firstname: this.firstname.toString(),
        lastname: this.lastname.toString(),
        code: this.code.toString()
      }
    };

    this.dynamicService.invokeGrain(request).subscribe(res => {
  if (res && res.success && res.resultJson) {
    // Parse JSON string into an array
    this.employees = JSON.parse(res.resultJson);

  } else {
    this.employees = [];
    console.error('Error fetching data:', res.message);
  }
});



    }
}
