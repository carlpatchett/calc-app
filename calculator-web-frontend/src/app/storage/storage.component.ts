import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-storage',
  templateUrl: './storage.component.html',
  styleUrls: ['./storage.component.css']
})
export class StorageComponent implements OnInit {
  private readonly mStorageConnectionString : string = "https://localhost:7276/OperationDatabaseStorage";

  latestResult: OperationForUi | undefined;

  operationToStore: OperationForUi | undefined;
  operations: OperationForUi[] | undefined;
  operation: OperationForUi | undefined;
  operationIdToGet: number | undefined;

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
  }

  GetLatest(): void {

    this.http.get<OperationFromDatabase>(`${this.mStorageConnectionString}/GetLatest`)
    .subscribe(result =>  {
      this.latestResult = {
        operator: ArithmeticOperators[result.operator],
        x: result.x,
        y: result.y,
        result: result.result
      };
    });
  }

  Get(id?: number): void {

    if (id === undefined) {
      this.operations = [];

      this.http.get<OperationFromDatabase[]>(`${this.mStorageConnectionString}/Get`)
        .subscribe(result =>
          {
            result.forEach(result => {
              this.operations?.push({
                operator: ArithmeticOperators[result.operator],
                x: result.x,
                y: result.y,
                result: result.result
              })
            });
          });

          return;
    }

    this.http.get<OperationFromDatabase>(`${this.mStorageConnectionString}/Get/${id}`)
    .subscribe(result =>  {
      this.operation = {
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
