import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'role'
})
export class RolePipe implements PipeTransform {

  transform(value: number): string {
    const roles: {
      [
      key: number
      ]: string
    } = {
      1: 'Admin', 2: 'User'
    }
    return roles[value];
  }

}
