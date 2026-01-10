use Test::More;
use A;

is('Hello, World', A::hello('World'), 'A::hello returns Hello, World');

done_testing();