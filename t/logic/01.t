use Test::More;

ok(1, 'true is true');
is(1 + 1, 2, 'math works');
like('hello', qr/ell/, 'regex match');

done_testing;