export interface isCorrectStatus {
    status: boolean,
    message: string
}
export interface IsCorrect {
    logLogin: isCorrectStatus,
    logPassword: isCorrectStatus,
    regUsername: isCorrectStatus,
    regEmail: isCorrectStatus,
    regPassword: isCorrectStatus,
    regPasswordConfirmation: isCorrectStatus
}

export interface LoginUserData {
    login: string,
    password: string,
}

export interface RegistrationUserData {
    username: string,
    email: string,
    name: string,
    surname: string,
    patronymic: string,
    password: string,
    passwordConfirmation: string
}

export interface AuthorizationState {
    loginUserData: LoginUserData,
    registrationUserData: RegistrationUserData,
    isCorrect: IsCorrect
}