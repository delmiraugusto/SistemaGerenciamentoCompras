export class User {
  id: number = -1;
  email: string = "";
  name: string = "";
  password: string = "";
  roleID?: number;
}

export class UserLogin {
  email: string = "";
  password: string = "";
}

export class UserLoginResponse {
  name: string = "";
  token: string = "";
  roleID?: number;
}

export class UserInsert {
  email: string = "";
  name: string = "";
  password: string = "";
  roleID?: number;
}

export class UserUpdate {
  id: number = -1;
  email: string = "";
  name: string = "";
  password: string = "";
  roleID?: number;
}