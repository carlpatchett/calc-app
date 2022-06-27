import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-storage',
  templateUrl: './storage.component.html',
  styleUrls: ['./storage.component.css']
})
export class StorageComponent implements OnInit {
  private readonly mStorageConnectionString : string = "https://localhost:7276/OperationDatabaseStorage";

  latestResult: OperationForUi | undefined;

  operationToStore: OperationForUi | undefined;

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
  }

  RetrieveLatestResult(): void {

    this.http.get<OperationFromDatabase>(`${this.mStorageConnectionString}/RetrieveLatestResult`)
    .subscribe(result =>  {
      this.latestResult = {
        operator: ArithmeticOperators[result.operator],
        x: result.x,
        y: result.y,
        result: result.result
      };
    });
  }

  Store(operation: OperationForUi): void {
    this.http.post(`${this.mStorageConnectionString}/Store`, operation).subscribe();
  }
}

interface OperationFromDatabase {
  operator: number;
  x: number;
  y: number;
  result: number;
}

interface OperationForUi {
  operator: string;
  x: number;
  y: number;
  result: number;
}

interface Operation {
  operator: ArithmeticOperators;
  x: number;
  y: number;
  result: number;
}

enum ArithmeticOperators
{
  Add,
  Subtract,
  Multiply,
  Divide
}
