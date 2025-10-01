import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Modal } from 'bootstrap'
import { EmployeeService } from './services/employee.service';

@Component({
  selector: 'app-employee',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

   constructor(private employeeService: EmployeeService) {
      
    }

  ngOnInit(): void {
    this.loadEmployees();
  }
  employees: any[] = [];

  filteredEmployees: any[] = [];
  @ViewChild('employeeModalRef') employeeModalRef: any;
selectedEmployee: any = null;

  loadEmployees(): void {
    this.employeeService.getEmployee().subscribe({
      next: (response) => {
        
        this.employees = response.data;
        this.filteredEmployees = [...this.employees];
      },
      error: (err) => {
      }
    });
  }



 filters: { 
    name: string; 
    department: string; 
    type: string; 
    itemsPerPage: number;
  } = {
    name: '',
    department: '',
    type: '',
    itemsPerPage: 5
  };



  departments = ['IT', 'HR', 'Sales', 'Admin'];
  employeeTypes = ['Permanent', 'Contract', 'Temporary'];

  addEmployee(emp: any) {
  this.employees.push(emp);
}

currentPage = 1;
totalPages = 1;

applyFilters() {
  let temp = this.employees.filter(emp => {
    const matchesName = this.filters.name
      ? emp.fullName.toLowerCase().includes(this.filters.name.toLowerCase())
      : true;
    const matchesDept = this.filters.department
      ? emp.department === this.filters.department
      : true;
    const matchesType = this.filters.type
      ? emp.employeeType === this.filters.type
      : true;
    return matchesName && matchesDept && matchesType;
  });


  const itemsPerPage = +this.filters.itemsPerPage; 
  this.totalPages = Math.ceil(temp.length / itemsPerPage);

  if (this.currentPage > this.totalPages) this.currentPage = this.totalPages || 1;


  const startIndex = (this.currentPage - 1) * itemsPerPage;
  const endIndex = startIndex + itemsPerPage;
  this.filteredEmployees = temp.slice(startIndex, endIndex);
}



goToPage(page: number) {
  if (page < 1) page = 1;
  if (page > this.totalPages) page = this.totalPages;
  this.currentPage = page;
  this.applyFilters();
}

nextPage() {
  this.goToPage(this.currentPage + 1);
}

previousPage() {
  this.goToPage(this.currentPage - 1);
}

openCreateModal() {
  this.selectedEmployee = null;
  this.employeeModalRef.open(); 
}


openEditModal(emp: any) {
  this.selectedEmployee = emp; 
  this.employeeModalRef.open();
}
 onDeleteEmployee(employee:any) {
  if (confirm('Are you sure you want to delete this employee?')) {
    this.employeeService.deleteEmployee(employee.employeeId).subscribe({
      next: (res) => {
        alert(res.message); // show success message
        this.loadEmployees(); // refresh the list after deletion
      },
      error: (err) => {
        
        alert('Failed to delete employee');
      }
    });
  }
}
}
