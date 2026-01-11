use strict;
use warnings;
use DateTime;

package MyApp::Util::Cal;

# date より前の指定した曜日の日付を求める
sub topday {
    my ($w, $year, $month) = @_;

    my $result = DateTime->new(
        year  => $year,
        month => $month,
        day   => 1,
    );

    return $result;
}

1;
