use Test::More;
use MyApp::A;

is('Hello, World', A::hello('World'), 'A::hello returns Hello, World');

done_testing;