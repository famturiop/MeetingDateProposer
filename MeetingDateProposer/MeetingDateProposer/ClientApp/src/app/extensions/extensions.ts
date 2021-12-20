interface String {
    isEmpty(): boolean;
}

String.prototype.isEmpty = function (this): boolean {
    return (!this || this.length === 0 );
}