use Test::More;
use MyApp::Util::Cal;
use DateTime;

ok(1, 'true is true');

my $expect = DateTime->new(
    year  => 2026,
    month => 2,
    day   => 1,
);

my $res1 = MyApp::Util::Cal::topday(7, 2026, 2);
is($res1, $expect, '日曜日始まりのカレンダーの 2026/2 月のトップは 2026/2/1');

done_testing;